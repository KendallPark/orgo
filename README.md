## Setup Instructions

1. Create a New Unity Project.
2. Close Unity.
3. Open up your [terminal](http://www.iterm2.com/) and navigate inside your Unity project directory.
4. Delete `Library`, `Assets`, `ProjectSettings` folders. `rm -r Library Assets ProjectSettings`
5. Initialize the directory as a Git repoository: `git init`
6. Add this repo as a remote: `git remote add origin git@github.com:KendallPark/orgo.git`
7. Fetch remote changes: `git fetch`
8. Checkout `dev` branch: `git checkout dev`
