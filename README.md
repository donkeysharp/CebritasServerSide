CebritasServerSide
==================

Server side services for Cebritas project, this will contain REST API for consume from mobile devices.

It will also have the monitor web site in which problems will be shown and official users can report a problem.
By official users we understand:
 * Media
 * Police
 * Town Hall

It will contain an internal embedded job(Quartz.Net) that will delete past problems, etc.


The methods for the Cebritas API are the next:

### PROBLEMS MODULE REST API

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
#### Get problems
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
      Latitude: {double: latitude from where the first report has been reported},
      Longitude: {double: longitude from where the first report has been reported},
      Type: {integer: type of the problem},
      Description: {string: first report description},
      ReportedAt: {long: unix time stamp in seconds},
      Reporters: {
        {
          FacebookCode: {string},
          Latiude: {double},
          Longitude: {double},
          Type: {integer},
          Description: {string},
          ReportedAt: {long}
        },
        {
          FacebookCode: {string},
          Latiude: {double},
          Longitude: {double},
          Type: {integer},
          Description: {string},
          ReportedAt: {long}
        },
        ...
      }
    },
    ...
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 400 -> response-message: "formato_coordenadas_incorrecto"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
#### Get friend's reported problems
<p>Get all today's problems from the user's facebook friends who use the same app.</p>
<b>URL:</b> http://example.com/api/problems/getbyfriends?friends={list_of_facebook_codes} <br>
<b>Method:</b> GET <br>
<b>Parameters:</b>
<ul>
  <li>
    <i>friends(string)</i> - contains a list of facebook codes from our friends who are using the same app, it must be in th format: friends={fb_code1},fb_code2,... e.g. friends=11111,22222,33333
  </li>
</ul>
<b>Response:</b>
```
{
  Status: [status-code],
  Message: [response-message],
  Data: [
    {
      FacebookCode: {string},
      Latiude: {double},
      Longitude: {double},
      Type: {integer},
      Description: {string},
      ReportedAt: {long}
    },
    {
      FacebookCode: {string},
      Latiude: {double},
      Longitude: {double},
      Type: {integer},
      Description: {string},
      ReportedAt: {long}
    },
    ...
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
### PLACES MODULE REST API

#### Get Categories
<p>Get categories tree, this application has a two level category tree, root category and children categories</p>
<b>URL:</b> http://example.com/api/places/getcategories <br>
<b>Method:</b> GET <br>
<b>Parameters:</b> None<br>
<b>Response:</b>
```
{
  "Status": [status-code],
  "Message": [response-message],
  "Data": [
    {
      "Code": {string},
      "Name": {string},
      "SpanishName": {string},
      "ParentCode": {string},
      "Icon": {string},
      "SubCategories": [
        {
          "Code": {string},
          "Name": {string},
          "SpanishName": {string},
          "ParentCode": {string},
          "Icon": string[]"
        },
        {
          "Code": {string},
          "Name": {string},
          "SpanishName": {string},
          "ParentCode": {string},
          "Icon": {string}
        },
        ...
      ]
    },
    {
      "Code": {string},
      "Name": {string},
      "SpanishName": {string},
      "ParentCode": {string},
      "Icon": {string},
      "SubCategories": []
    },
    ...
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
#### Get Places by Category
<p>Get Places that belongs to a root category or category that has no parent in a default radius of 3000mt or 3km</p>
<b>URL:</b> http://example.com/api/places/getbycategory?code={code}&latitude={latitude}&longitude={longitude} <br>
<b>Method:</b> GET <br>
<b>Parameters:</b>
<ul>
  <li>
    <i>code(string)</i> - root category count, if this code belongs to a child category result will be empty
  </li>
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
  "Status": [status-code],
  "Message": [response-message],
  "Data": [
    {
      "Code": {string},
      "Name": {string},
      "Address": {string},
      "WebSite": {string},
      "MinPrice": {integer},
      "MaxPrice": {integer},
      "Parking": {boolean},
      "Holidays": {boolean},
      "SmokingArea": {boolean},
      "KidsArea": {boolean},
      "Delivery": {boolean},
      "Rating": {integer},
      "Latitude": {double},
      "Longitude": {double},
      "CategoryCode": {string}
    },
    ...
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 400 -> response-message: "formato_coordenadas_incorrecto"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
#### Get Near Places by Category
<p>Get near Places that belongs to a root category or category that has no parent</p>
<b>URL:</b> http://example.com/api/places/getbycategorynear?code={code}&latitude={latitude}&longitude={longitude}&radius={radius} <br>
<b>Method:</b> GET <br>
<b>Parameters:</b>
<ul>
  <li>
    <i>code(string)</i> - root category count, if this code belongs to a child category result will be empty
  </li>
  <li>
    <i>latitude(double)</i> - latitude where the user is at, it must be in the en-US format e.g. 123.3232; 0.32; -16.232412 and not like 16,232; 32,232
  </li>
  <li>
    <i>longitude(double)</i> - longitude where the user is at it has the same latitude's format
  </li>
  <li>
    <i>radius(integer)</i> - radius in meters that can be considered as "near", it must be greater than 3000, if not specified it will use the default value of 3000mt
  </li>
</ul>
<b>Response:</b>
```
{
  "Status": [status-code],
  "Message": [response-message],
  "Data": [
    {
      "Code": {string},
      "Name": {string},
      "Address": {string},
      "WebSite": {string},
      "MinPrice": {integer},
      "MaxPrice": {integer},
      "Parking": {boolean},
      "Holidays": {boolean},
      "SmokingArea": {boolean},
      "KidsArea": {boolean},
      "Delivery": {boolean},
      "Rating": {integer},
      "Latitude": {double},
      "Longitude": {double},
      "CategoryCode": {string}
    },
    ...
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 400 -> response-message: "formato_coordenadas_incorrecto"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
#### Get Places by query (name)
<p>Get Places whose name is similar to the query parameter</p>
<b>URL:</b> http://example.com/api/places/getbyquery?query={query}&latitude={latitude}&longitude={longitude} <br>
<b>Method:</b> GET <br>
<b>Parameters:</b>
<ul>
  <li>
    <i>query(string)</i> - this value will be a search pattern based in name i.e. if query is "car" will match with "carpet", "aacardsdw", etc.
  </li>
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
  "Status": [status-code],
  "Message": [response-message],
  "Data": [
    {
      "Code": {string},
      "Name": {string},
      "Address": {string},
      "WebSite": {string},
      "MinPrice": {integer},
      "MaxPrice": {integer},
      "Parking": {boolean},
      "Holidays": {boolean},
      "SmokingArea": {boolean},
      "KidsArea": {boolean},
      "Delivery": {boolean},
      "Rating": {integer},
      "Latitude": {double},
      "Longitude": {double},
      "CategoryCode": {string}
    },
    ...
  ]
}
status-code: 200 -> response-message: "ok"
status-code: 400 -> response-message: "formato_coordenadas_incorrecto" | "query_param_is_required"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
