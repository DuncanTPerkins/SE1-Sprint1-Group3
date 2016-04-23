using FormBuilderApp.Models;
using System.Data.Entity;

public class FormBuilderDb : DbContext
{
    public FormBuilderDb()
        : base("name=DefaultConnection")
    {
    }

    public DbSet<Form> form { get; set; }
    public DbSet<Workflow> flow { get; set; }
    public DbSet<Positions> position { get; set; }
}