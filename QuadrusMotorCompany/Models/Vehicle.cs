using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace QuadrusMotorCompany.Models
{
    public class Vehicle
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string Description { get; set; }

        #endregion


        #region Relationships

        public virtual ICollection<File> Files { get; set; }

        #endregion

    }
}