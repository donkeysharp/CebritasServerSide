CebritasServerSide
==================

Server side services for Cebritas project, this will contain REST API for consume from mobile devices.

It will also have the monitor web site in which problems will be shown and official users can report a problem.
By official users we understand:
 * Media
 * Police
 * Town Hall

It will contain an internal embedded job(Quartz.Net) that will delete past problems, etc.


### PROBLEMS MODULE REST API

The methods for the Cebritas API are the next:

#### Report Problem

<b>URL:</b> http://example.com/api/problems/report <br>
<b>Method:</b> POST <br>
<b>Data:</b>
<ul>
  <li>
    <i>facebookcode(string)</i> - as the application will use facebook authentication, the user's facebook code will be necessary
  </li>
  <li>
    <i>latitude(double)</i> - latitude from where the problem has been reported, it must be in the en-US format e.g. 123.3232; 0.32; -16.232412 and not like 16,232; 32,232
  </li>
  <li>
    <i>longitude(double)</i> - longitude from where the problem has been reported, it has the same latitude's format
  </li>
  <li>
    <i>description(string)</i> - it's the problems description described by the user
  </li>
  <li>
    <i>type(integer)</i> - it's the type of the problem. This number has the next "meaning" mapping:
    <ol>
      <li>Traffic problem</li>
      <li>Manifestation problem</li>
      <li>Parade</li>
      <li>Block</li>
      <li>Accident</li>
      <li>Others</li>
    </ol>
  </li> 
</ul>
<b>Response:</b>
```
{
  Status: [status-code],
  Message: [response-message],
  Data: []
}
status-code: 200 -> response-message: "ok"
status-code: 400 -> response-message: "formato_coordenadas_incorrecto | user_has_already_reported_here"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
#### Report Problem
<p>Get all today's problems around the user's position with a radio of 20 kilometers</p>
<b>URL:</b> http://example.com/api/problems/get?latitude={latitude}&longitude={longitude} <br>
<b>Method:</b> GET <br>
<b>Parameters:</b>
<ul>
  <li>
    <i>latitude(double)</i> - latitude where the user is at, it must be in the en-US format e.g. 123.3232; 0.32; -16.232412 and not like 16,232; 32,232
  </li>
  <li>
    <i>longitude(double)</i> - longitude where the user is at it has the same latitude's format
  </li>
</ul>
<b>Response:</b>
```
{
  Status: [status-code],
  Message: [response-message],
  Data: [
    {
      Code: {string: problem code},
      FacebookCode: {string: first reporter's facebook code}
      Importance: {integer: how many people reported},
      Verified: {boolean: whether or not the problem is officially verified},
      Latitude: {string: latitude from where the first report has been reported},
      Longitude: {string: longitude from where the first report has been reported},
      Type: {integer: type of the problem},
      Description: {string: first report description},
      ReportedAt: {long: unix time stamp in seconds},
      Reporters: {
        {
          FacebookCode: {string},
          Latiude: {string},
          Longitude: {string},
          Type: {integer},
          Description: {string},
          ReportedAt: {long}
        },
        {
          FacebookCode: {string},
          Latiude: {string},
          Longitude: {string},
          Type: {integer},
          Description: {string},
          ReportedAt: {long}
        },
        ...
      }
    }
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 400 -> response-message: "formato_coordenadas_incorrecto"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
