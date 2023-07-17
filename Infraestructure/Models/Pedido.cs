//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    //[MetadataType(typeof(LibroMetadata))]
    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            this.PedidoProducto = new HashSet<PedidoProducto>();
        }

        public int id { get; set; }
        public int usuarioId { get; set; }
        public int direccionId { get; set; }
        public int estadoPedidoId { get; set; }
        public int metodoPagoId { get; set; }
        public double total { get; set; }
        public System.DateTime fecha_pedido { get; set; }

        public virtual Direccion Direccion { get; set; }
        public virtual EstadoPedido EstadoPedido { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoProducto> PedidoProducto { get; set; }
    }
}
