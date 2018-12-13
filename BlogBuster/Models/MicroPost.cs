using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace BlogBuster.Models
{
    public class MicroPost
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; }

        [Required]
        public int User_Id { get; private set; }

        public MicroPost(string Content, int User_Id)
        {
            this.Content = Content;
            this.User_Id = User_Id;
        }

        public MicroPost(int Id, string Content, int User_Id)
        {
            this.Id = Id;
            this.Content = Content;
            this.User_Id = User_Id;
        }

        public static List<MicroPost> All()
        {
            List<MicroPost> microposts = new List<MicroPost>();

            DataTable dataTable = SqlConnectionGenerator.ExecuteStoreProcedure("sp_show_all_microposts_from_newer");

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                microposts.Add(new MicroPost
                (
                    int.Parse(dataTable.Rows[row][0].ToString()),
                    dataTable.Rows[row][1].ToString(),
                    int.Parse(dataTable.Rows[row][2].ToString())
                ));
            }

            return microposts;
        }

        public static MicroPost Find(int Id)
        {
            DataTable dataTable = SqlConnectionGenerator.ExecuteStoreProcedure("sp_show_micropost",
                new string[] { "@MicroPost_Id" },
                new object[] { Id }
                );

            return new MicroPost(
                int.Parse(dataTable.Rows[0][0].ToString()),
                dataTable.Rows[0][1].ToString(),
                int.Parse(dataTable.Rows[0][2].ToString())
                );
        }

        public bool Save()
        {
            try
            {
                if (ItExists())
                    SqlConnectionGenerator.ExecuteStoreProcedure("sp_update_micropost",
                        new string[] { "@MicroPost_Id", "@Content" },
                        new object[] { this.Id, this.Content }
                        );
                else
                    SqlConnectionGenerator.ExecuteStoreProcedure("sp_create_micropost",
                        new string[] { "@Content", "@User_Id"  },
                        new object[] { this.Content, this.User_Id }
                        );

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        private bool ItExists()
        {
            try
            {
                return (MicroPost.Find(this.Id).Id > 0);
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                SqlConnectionGenerator.ExecuteStoreProcedure("sp_delete_micropost",
                    new string[] { "@MicroPost_Id" },
                    new object[] { this.Id }
                    );
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

    }
}