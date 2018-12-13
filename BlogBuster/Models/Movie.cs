using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace BlogBuster.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(254)]
        public string Title { get; set; }

        [Required]
        [StringLength(254)]
        public string Description { get; set; }
        
        [Required]
        public int User_Id { get; set; }

        public Movie(string Title, string Description, int User_Id)
        {
            this.Title = Title;
            this.Description = Description;
            this.User_Id = User_Id;
        }

        public Movie(int Id, string Title, string Description, int User_Id)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.User_Id = User_Id;
        }



        public static Movie Find(int Id)
        {
            DataTable dataTable = SqlConnectionGenerator.ExecuteStoreProcedure("sp_show_movie",
                new string[] { "@Movie_Id" },
                new object[] { Id }
                );

            return new Movie(
                int.Parse(dataTable.Rows[0][0].ToString()),
                dataTable.Rows[0][1].ToString(),
                dataTable.Rows[0][2].ToString(),
                int.Parse(dataTable.Rows[0][3].ToString())
                );
        }

        public bool Save()
        {
            try
            {
                if (ItExists())
                    SqlConnectionGenerator.ExecuteStoreProcedure("sp_update_movie",
                        new string[] { "@Movie_Id", "@Title", "@Content" },
                        new object[] { this.Id, this.Title, this.Description }
                        );
                else
                    SqlConnectionGenerator.ExecuteStoreProcedure("sp_create_movie",
                        new string[] { "@Title", "@Description", "@User_Id" },
                        new object[] { this.Title, this.Description, this.User_Id }
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
                return (Movie.Find(this.Id).Id > 0);
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
                SqlConnectionGenerator.ExecuteStoreProcedure("sp_delete_movie",
                    new string[] { "@Movie_Id" },
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