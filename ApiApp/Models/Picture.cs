using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiApp.Models;

[SQLite.Table("picture")]

public class Picture
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [SQLite.MaxLength(250), Unique]
    public string Name { get; set; }


}