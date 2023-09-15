using Dapper;
using System.Data.SqlClient;
namespace GFL.Data
{
    public class FolderRepo : IDisposable
    {
        private readonly SqlConnection sql;
        public FolderRepo(SqlConnection sql)
        {
            this.sql = sql;
            sql.Open();
        }

        public void Dispose()
        {
            sql.Dispose();
        }

        public IEnumerable<Folder>? GetFolderHierarcy()
        {
            return sql.Query<Folder>("SELECT * FROM Folder WHERE Parent_ID is NULL");
        }
        public IEnumerable<Folder>? GetInnerFolders(Guid id)
        {
            return sql.Query<Folder>("SELECT * FROM Folder WHERE Parent_ID = @Parend_Id", new { Parend_Id = id });
        }
        public Folder? GetFullFolder(Guid id)
        {
            var Folder = sql.QuerySingle<Folder>("SELECT * FROM Folder WHERE Folder_Id = @Folder_Id", new { Folder_Id = id });
            FillInnerFolders(Folder);
            return Folder;
        }

        private void FillInnerFolders(Folder folder)
        {
            folder.InnerFolders = GetInnerFolders(folder.Folder_id);
            if (folder.InnerFolders is null) return;
            foreach (var item in folder.InnerFolders)
            {
                FillInnerFolders(item);
            }
        }


        public void InsertAll(Folder folder)
        {
            folder.Folder_id = Guid.NewGuid();
            sql.Execute("INSERT INTO Folder VALUES (@Folder_Id, @Parent_Id, @Name)",
                new { folder.Folder_id, folder.Parent_id, folder.Name });
            foreach (var innerFolder in folder.InnerFolders)
            {
                innerFolder.Parent_id = folder.Folder_id;
                InsertAll(innerFolder);
            }
        }
    }
}
