# descarga-ciec-csharp

API sencilla para interactuar con el servicio de descarga masiva mediante CIEC de CSFacturación



## Installation

Install descarga_ciec_csharp

```bash
dotnet add package descarga-ciec-csharp --version 1.1.3
```
 ## Implementation

 ### Usage
 

 #### Inicio Rápido


```bash

descarga_ciec_sdk.DescargaCIEC DescragaCIEC = new descarga_ciec_sdk.DescargaCIEC();
ConsultaParametrosBuilder parametrosBuilder = new ConsultaParametrosBuilder();

// credenciales empresa ante el SAT
Empresa empresa = new Empresa("rfcEmpresa", "credencial_SAT");

// credenciales de contratacion CSFacturacion
User user = new User("rfcContratacion", "password");

// construir parametros de consulta con ParametrosBuilder
parametrosBuilder.setCredecialesContratacion(user);
parametrosBuilder.setCredecialesSAT(empresa);
parametrosBuilder.setFechaInicio(new DateTime(2024, 2, 1));
parametrosBuilder.setFechaFin(new DateTime(2024, 3, 3));
parametrosBuilder.setMovimiento(descarga_ciec_sdk.src.Enums.Movimiento.TODAS);
parametrosBuilder.setFiltroEstatusCFDI(descarga_ciec_sdk.src.Enums.EstatusCFDI.TODOS;
parametrosBuilder.setTipoDocumento(descarga_ciec_sdk.src.Enums.TipoDocumento.CFDI);

//Parametros               
ConsultaParametros parametrosConsulta = new ConsultaParametros(parametrosBuilder);

//Solicitar la consulta de descarga
var folioConsulta = DescragaCIEC.SolicitarConsulta(parametrosConsulta);


```


#### Progreso 

Despues de solicitar la descarga se puede obtener el estatus y el total encontrado usando el folio de la consulta

```bash

 var progreso = DescargaCIEC.GetProgreso(folioConsulta);


 while (!progreso.IsCompletado())
 {
      Console.WriteLine("Estatus : ", progreso.GetStatus());
      Console.WriteLine("Total XMLs : ", progreso.GetEncontrado());

 }

```

#### Resumen 

Se puede obtener el resumen de la consulta siempre y cuando este completado (con error o sin error) usando el folio de la consulta

```bash


var resumen = DescargaCIEC.GetSummary(folioConsulta);

 Console.WriteLine("Total XMLs: ", resumen.total);
 Console.WriteLine("Total Paginas: ", resumen.paginas);
 Console.WriteLine("Hay XMLs Faltantes: ",resumen.xmlFaltantes);
 Console.WriteLine("Hay XMLs con fechasMismoHorario: ",resumen.fechasMismoHorario);
 Console.WriteLine("Total XMLs Cancelados: ",resumen.cancelados);

```


#### Resultados 

Para la obtención de los resultados usa los siguientes metodos usando el folio de la consulta.
1-Mediante paginas (Metadata)
2-Zip
3-Lista metadata

```bash

//Para obtener resultado mediante paginas
var metadata = DescargaCIEC.GetResultado(folioConsulta, numeroPgaina);

// Usando el resumen
for (int i = 0; i < DescargaCIEC.getSummary().paginas(); i++)
 {

    var resultadosCFDI = DescargaCIEC.GetResultado(folioConsulta, i);
    foreach (var metadata in resultadosCFDI)
    {
        // Metadata XML
    }
}

//Para obtener el de los XMLs mediante paginas
var path = DescargaCIEC.DescargaZIP(folioConsulta, RutaZip);


//Para obtener la lista de metadata 
var listaMetada = DescargaCIEC.DescargaMetadataXml(folioConsulta);

```

#### Para más ejemplos, ver el archivo de prueba
```bash


//Descargar  ZIP y descomprimirlo usando el folio de la consulta
 var path = DescargaCIEC.DescargarAndDescomprimirZIP(folioConsulta, RutaZip);

```




## Acknowledgements

 - [Descarga CIEC C# SDK](https://github.com/ConroeSoluciones/CSReporter-WS-CSharp)
 - [API Descarga CIEC](https://docs.csfacturacion.com/descarga-masiva/ciec/introduction/)
 - [Unit testing C# in .NET using dotnet test and xUnit](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)


## API Reference

#### 
 - [API Descarga CIEC](https://docs.csfacturacion.com/descarga-masiva/ciec/introduction/)




## Appendix

- 500 Mismo Segundo: 
-  XMLs Faltantes
- Limite de descargas
## Authors

- [@csfacturacion](https://csfacturacion.com/)


## Tags

[SAT](https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0)

[Descarga Masiva](https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0)


[CIEC](https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0)
