using System.IO;
using System.Reflection;
using FubuCore;

namespace Bottles.Services.Remote
{
    public class AssemblyRequirement
    {
        private readonly static FileSystem fileSystem = new FileSystem();
        private readonly Assembly _assembly;

        public AssemblyRequirement(string name)
        {
            _assembly = Assembly.Load(name);
        }

        public AssemblyRequirement(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Move(string directory)
        {
            var location = _assembly.Location;

            var fileName = Path.GetFileName(location);



            if (!fileSystem.FileExists(directory.AppendPath(fileName)))
            {
                fileSystem.CopyToDirectory(location, directory);
            }



            var pdb = Path.GetFileNameWithoutExtension(fileName) + ".pdb";
            fileSystem.CopyToDirectory(location.ParentDirectory().AppendPath(pdb), directory);
        }
    }
}