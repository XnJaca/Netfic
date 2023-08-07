using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelOrdenDetalle
    {
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public byte[] Imagen { get; set; }
        public int Cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Precio
        {
            get { return Producto.precio; }

        }
        public virtual Producto Producto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }
        private double calculoSubtotal()
        {
            return this.Precio * this.Cantidad;
        }


        public ViewModelOrdenDetalle(int idProducto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            this.IdProducto = idProducto;
            this.Producto = _ServiceProducto.GetProductoById(idProducto);
        }
    }
}