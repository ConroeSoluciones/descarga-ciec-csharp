# descarga-ciec-csharp

API sencilla para interactuar con el servicio de descarga masiva mediante CIEC de CSFacturación en C#.



## Installation

 Install descarga_ciec_csharp using Package Manager

```bash
 NuGet\Install-Package descarga-ciec-csharp
```

 Install descarga_ciec_csharp using .NET CLI

```bash
dotnet add package descarga-ciec-csharp
```
 ## Implementation

 ### Usage
 

 #### Inicio Rápido


```bash

DescargaCIEC descargaCIEC = new DescargaCIEC();
ConsultaParametrosBuilder parametrosBuilder = new ConsultaParametrosBuilder();

// credenciales empresa ante el SAT
Credenciales credenciales = new Credenciales("rfcEmpresa", "credencial_SAT");

// credenciales de contratacion CSFacturacion
User user = new User("rfcContratacion", "password");

// construir parametros de consulta con ParametrosBuilder
parametrosBuilder.setCredecialesContratacion(user);
parametrosBuilder.setCredecialesSAT(credenciales);
parametrosBuilder.setFechaInicio(new DateTime(2024, 2, 1));
parametrosBuilder.setFechaFin(new DateTime(2024, 3, 3));
parametrosBuilder.setMovimiento(descarga_ciec_sdk.src.Enums.Movimiento.TODAS);
parametrosBuilder.setFiltroEstatusCFDI(descarga_ciec_sdk.src.Enums.EstatusCFDI.TODOS);
parametrosBuilder.setTipoDocumento(descarga_ciec_sdk.src.Enums.TipoDocumento.CFDI);

//Parametros               
ConsultaParametros parametrosConsulta = new ConsultaParametros(parametrosBuilder);

//Solicitar la consulta de descarga
var folioConsulta = descargaCIEC.SolicitarConsulta(parametrosConsulta);




```


#### Progreso 

Despues de solicitar la descarga se puede obtener el estatus y el total encontrado usando el folio de la consulta

```bash

 var progreso = descargaCIEC.GetProgreso(folioConsulta);


 while (!progreso.IsCompletado())
 {
   // Espera hasta que se termine osea completado
      Console.WriteLine($"Estatus : {progreso.GetStatus()} ");
      Console.WriteLine($"Total XMLs : {progreso.GetEncontrado()}");
 }

```

#### Resumen 

Se puede obtener el resumen de la consulta siempre y cuando este completado (con error o sin error) usando el folio de la consulta

```bash


//Resumen

var resumen = descargaCIEC.GetSummary(folioConsulta);

Console.WriteLine($"Total XMLs: {resumen.total}");
Console.WriteLine($"Total Paginas: {resumen.paginas}");
Console.WriteLine($"Hay XMLs Faltantes: {resumen.xmlFaltantes}");
Console.WriteLine($"Hay XMLs con fechasMismoHorario: {resumen.fechasMismoHorario.Count}");
Console.WriteLine($"Total XMLs Cancelados:  {resumen.cancelados}");

```


#### Resultados 

Para la obtención de los resultados usa los siguientes metodos usando el folio de la consulta.
1-Mediante paginas (Metadata)
2-Zip
3-Lista metadata

```bash


//Para obtener resultado mediante paginas
// El uno (1) es el numero de pagina
var metadata = descargaCIEC.GetResultado(folioConsulta, 1);

// Usando el resumen
for (int i = 1; i < descargaCIEC.GetSummary(folioConsulta).paginas; i++)
{

    var resultadosCFDI = descargaCIEC.GetResultado(folioConsulta, i);
    foreach (var meta in resultadosCFDI)
    {
        // Metadata XML
         Console.WriteLine($"Metadata UUID: { meta.folio}");

    }
}

//Para obtener el de los XMLs mediante paginas
//Hay que tener en cuenta que se debe pasar la runta de la carpeta donde se guardara el ZIP
var path = descargaCIEC.DescargaZIP(folioConsulta, "RutaZip");


//Para obtener la lista de metadata 
var listaMetada = descargaCIEC.GetListMetadata(folioConsulta);

```

#### Para más ejemplos, ver el archivo de prueba
```bash


//Descargar  ZIP y descomprimirlo usando el folio de la consulta
 var path = descargaCIEC.DescargarAndDescomprimirZIP(folioConsulta, "RutaZip");

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

[CFDI](https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0)

[CIEC](https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0)
