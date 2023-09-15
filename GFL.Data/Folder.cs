namespace GFL.Data
{
    public class Folder
    {
        public Guid Folder_id { get; set; }
        public Guid? Parent_id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Folder> InnerFolders { get; set; } = Enumerable.Empty<Folder>();
    }
}
