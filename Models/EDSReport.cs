using System;
using System.Collections.Generic;

namespace Models
{
    public class EDSReport
    {
        public string Version { get; set; }
        public string RfcContribuyente { get; set; }
        public string RfcRepresentanteLegal { get; set; }
        public string RfcProveedor { get; set; }
        public string Caracter { get; set; }
        public string ModalidadPermiso { get; set; }
        public string NumPermiso { get; set; }
        public string ClaveInstalacion { get; set; }
        public string DescripcionInstalacion { get; set; }

        public int NumeroPozos { get; set; }
        public int NumeroTanques { get; set; }
        public int NumeroDuctosEntradaSalida { get; set; }
        public int NumeroDuctosTransporteDistribucion { get; set; }
        public int NumeroDispensarios { get; set; }

        public DateTimeOffset? FechaYHoraCorte { get; set; }

        public List<Producto> Producto { get; set; }
    }

    public class Producto
    {
        public string ClaveProducto { get; set; }
        public string ClaveSubProducto { get; set; }
        public decimal? ComposOctanajeGasolina { get; set; }
        public string GasolinaConCombustibleNoFosil { get; set; }

        public List<Tanque> Tanque { get; set; }
    }

    public class Tanque
    {
        public string ClaveIdentificacionTanque { get; set; }
        public string Localizaciony_o_DescripcionTanque { get; set; } // (si tu JSON usa "Localizaciony/oDescripcionTanque", lo mapeará case-insensitive, pero el nombre exacto aquí no importa para el flatten)
        public string Localizaciony_o_DescripcionTanque2
        {
            get => Localizaciony_o_DescripcionTanque;
            set => Localizaciony_o_DescripcionTanque = value;
        }

        public string VigenciaCalibracionTanque { get; set; }

        public UnidadValor CapacidadTotalTanque { get; set; }
        public UnidadValor CapacidadOperativaTanque { get; set; }
        public UnidadValor CapacidadUtilTanque { get; set; }
        public UnidadValor CapacidadFondajeTanque { get; set; }
        public UnidadValor VolumenMinimoOperacion { get; set; }

        public string EstadoTanque { get; set; }

        public List<Medidor> Medidores { get; set; }

        public Existencias Existencias { get; set; }
        public Recepciones Recepciones { get; set; }
        public Entregas Entregas { get; set; }
    }

    public class UnidadValor
    {
        public string UnidadDeMedida { get; set; }
        public decimal ValorNumerico { get; set; }
    }

    public class Medidor
    {
        public string SistemaMedicionTanque { get; set; }
        public string LocalizODescripSistMedicionTanque { get; set; }
        public string VigenciaCalibracionSistMedicionTanque { get; set; }
        public decimal? IncertidumbreMedicionSistMedicionTanque { get; set; }
    }

    public class Existencias
    {
        public decimal? VolumenExistenciasAnterior { get; set; }
        public UnidadValor VolumenAcumOpsRecepcion { get; set; }
        public string HoraRecepcionAcumulado { get; set; }
        public UnidadValor VolumenAcumOpsEntrega { get; set; }
        public string HoraEntregaAcumulado { get; set; }
        public decimal? VolumenExistencias { get; set; }
        public DateTimeOffset? FechaYHoraEstaMedicion { get; set; }
        public DateTimeOffset? FechaYHoraMedicionAnterior { get; set; }
    }

    public class Recepciones
    {
        public int? TotalRecepciones { get; set; }
        public UnidadValor SumaVolumenRecepcion { get; set; }
        public int? TotalDocumentos { get; set; }
        public decimal? SumaCompras { get; set; }
        public List<Recepcion> Recepcion { get; set; }
    }

    public class Recepcion
    {
        public int? NumeroDeRegistro { get; set; }
        public UnidadValor VolumenInicialTanque { get; set; }
        public decimal? VolumenFinalTanque { get; set; }
        public UnidadValor VolumenRecepcion { get; set; }
        public decimal? Temperatura { get; set; }
        public decimal? PresionAbsoluta { get; set; }
        public DateTimeOffset? FechaYHoraInicioRecepcion { get; set; }
        public DateTimeOffset? FechaYHoraFinalRecepcion { get; set; }
        public Complemento Complemento { get; set; }
    }

    public class Entregas
    {
        public int? TotalEntregas { get; set; }
        public UnidadValor SumaVolumenEntregado { get; set; }
        public int? TotalDocumentos { get; set; }
        public decimal? SumaVentas { get; set; }
        public List<Entrega> Entrega { get; set; }
    }

    public class Entrega
    {
        public long? NumeroDeRegistro { get; set; }
        public UnidadValor VolumenInicialTanque { get; set; }
        public decimal? VolumenFinalTanque { get; set; }
        public UnidadValor VolumenEntregado { get; set; }
        public decimal? Temperatura { get; set; }
        public decimal? PresionAbsoluta { get; set; }
        public DateTimeOffset? FechaYHoraInicialEntrega { get; set; }
        public DateTimeOffset? FechaYHoraFinalEntrega { get; set; }
        public Complemento Complemento { get; set; }
    }

    public class Complemento
    {
        public string TipoComplemento { get; set; }
        public string Aclaracion { get; set; }
        public List<Nacional> Nacional { get; set; }
    }

    public class Nacional
    {
        public string RfcClienteOProveedor { get; set; }
        public string NombreClienteOProveedor { get; set; }
        public List<CfdiInfo> CFDIs { get; set; }
    }

    public class CfdiInfo
    {
        public string Cfdi { get; set; }
        public string TipoCfdi { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioDeVentaAlPublico { get; set; }
        public decimal? PrecioVenta { get; set; }
        public DateTimeOffset? FechaYHoraTransaccion { get; set; }
        public UnidadValor VolumenDocumentado { get; set; }
    }
}

