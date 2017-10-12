using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ims.DataAccess.Repository
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        object IEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (T) value; }
        }
    }
}