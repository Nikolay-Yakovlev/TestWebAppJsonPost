using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entityes
{
    public class JsonFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FieldName{ get; set; }
    }
}
