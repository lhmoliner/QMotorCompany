using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuadrusMotorCompany.Models
{
    public class File
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public FileType FileType { get; set; }

        #endregion


        #region Relationships

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        #endregion
    } 
}