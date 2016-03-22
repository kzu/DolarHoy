# DolarHoy

Una forma conveniente de chequear el valor del dólar hoy

Uso:

* [http://dolarhoy.azurewebsites.net](http://dolarhoy.azurewebsites.net): devuelve valor de compra y venta como texto
* [http://dolarhoy.azurewebsites.net/?venta](http://dolarhoy.azurewebsites.net/?venta): devuelve valor de venta solo como texto
* [http://dolarhoy.azurewebsites.net/?compra](http://dolarhoy.azurewebsites.net/?compra): devuelve valor de compra solo como texto
* [http://dolarhoy.azurewebsites.net/?json](http://dolarhoy.azurewebsites.net/?json): devuelve valor de compra y venta como json


Este servicio combinado con Azure Logic Apps genera los tweets de [@dolarhoybot](http://twitter.com/dolarhoybot).

La información sale del sitio de [La Nacion - Dolar Hoy](http://www.lanacion.com.ar/dolar-hoy-t1369) en el momento 
en que se pide la página.
