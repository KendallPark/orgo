require 'pry'
require 'net/http'
require 'json'

def get_properties(cid)
  uri = URI("http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/cid/#{cid}/property/MolecularFormula,MolecularWeight,IUPACName/json")
  json = Net::HTTP.get(uri)
  data = JSON.parse(json)
  data["PropertyTable"]["Properties"].first
end

def get_description(cid)
  uri = URI("http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/cid/#{cid}/description/json")
  json = Net::HTTP.get(uri)
  data = JSON.parse(json)
  data["InformationList"]["Information"].first
end

def get_conformers(cid)
  uri = URI("http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/cid/#{cid}/conformers/json")
  json = Net::HTTP.get(uri)
  data = JSON.parse(json)
  data["InformationList"]["Information"].first["ConformerID"]
end

def get_common_names(cid)
  uri = URI("http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/cid/#{cid}/synonyms/json")
  json = Net::HTTP.get(uri)
  data = JSON.parse(json)
  synonyms = data["InformationList"]["Information"].first["Synonym"]
  { common_name: synonyms.first, common_names: synonyms.first(5) }
end

def get_atoms(data)
  elements = data["PC_Compounds"].first["atoms"]["element"]
  xpos = data["PC_Compounds"].first["coords"].first["conformers"].first["x"]
  ypos = data["PC_Compounds"].first["coords"].first["conformers"].first["y"]
  zpos = data["PC_Compounds"].first["coords"].first["conformers"].first["z"]
  atoms = elements.each_with_index.map do |element, i|
    {
      aid: i+1,
      element: element,
      x: xpos[i],
      y: ypos[i],
      z: zpos[i]
    }
  end
  Hash[*atoms.map{|a| [a[:aid], a] }.flatten]
end

def get_bonds(data)
  aids1 = data["PC_Compounds"].first["bonds"]["aid1"]
  aids2 = data["PC_Compounds"].first["bonds"]["aid2"]
  orders = data["PC_Compounds"].first["bonds"]["order"]
  bonds = aids1.each_with_index.map do |aid1, i|
    {
      bid: i+1,
      vertices: [aid1, aids2[i]],
      order: orders[i]
    }
  end
  Hash[*bonds.map{|b| [b[:bid], b] }.flatten]
end

def get_structure(cid)
  uri = URI("http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/cid/#{cid}/json?record_type=3d")
  json = Net::HTTP.get(uri)
  data = JSON.parse(json)
  atoms = get_atoms(data)
  bonds = get_bonds(data)
  { atoms: atoms, bonds: bonds }
end

def get_compound(cid)
  properties = get_properties(cid)
  description = get_description(cid)
  common_names = get_common_names(cid)
  # conformers = get_conformers(cid)
  structure = get_structure(cid)
  {
    cid: properties["CID"],
    name: common_names[:common_name],
    properties: properties.merge(common_names),
    description: description,
    structure: structure
  }
end

def save_compound(compound)
  File.open("../Compounds/#{compound[:cid]}.json","w") do |f|
    f.write(compound.to_json)
  end
  compound[:name]
end

def get_and_save_compound(cid)
  save_compound(get_compound(cid))
end

if ARGV[1] && ARGV[0]
  (ARGV[0]..ARGV[1]).each do |i|
    begin
      puts ""
      puts "importing compound #{i}"
      compound_name = get_and_save_compound(i)
      puts "successfully imported #{compound_name}!"
    rescue NoMethodError => e
      puts "could not import compound #{i} because:"
      puts e.message  
      puts e.backtrace.inspect
    end
  end
elsif ARGV[0]
  get_and_save_compound(ARGV[0])
end
