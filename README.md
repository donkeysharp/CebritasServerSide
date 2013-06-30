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
  <li>
    <i>timezone(integer)</i> - it's the reporter's timezone in which time will be reported, the mapping is the next:
    <ol>
<li>(UTC-12:00) Línea internacional de cambio de fecha          </li>
<li>(UTC-11:00) Hora universal coordinada-11                    </li>
<li>(UTC-10:00) Hawai                                           </li>
<li>(UTC-09:00) Alaska                                          </li>
<li>(UTC-08:00) Baja California                                 </li>
<li>(UTC-08:00) Hora del Pacífico (EE.UU. y Canadá)             </li>
<li>(UTC-07:00) Arizona                                         </li>
<li>(UTC-07:00) Chihuahua, La Paz, Mazatlán                     </li>
<li>(UTC-07:00) Hora de las Montañas Rocosas (EE.UU. y Canadá)  </li>
<li>(UTC-06:00) América Central                                 </li>
<li>(UTC-06:00) Guadalajara, Ciudad de México, Monterrey        </li>
<li>(UTC-06:00) Hora central (EE.UU. y Canadá)                  </li>
<li>(UTC-06:00) Saskatchewan                                    </li>
<li>(UTC-05:00) Bogotá, Lima, Quito                             </li>
<li>(UTC-05:00) Hora del este (EE.UU. y Canadá)                 </li>
<li>(UTC-05:00) Indiana (este)                                  </li>
<li>(UTC-04:30) Caracas                                         </li>
<li>(UTC-04:00) Asunción                                        </li>
<li>(UTC-04:00) Cuiabá                                          </li>
<li>(UTC-04:00) Georgetown, La Paz, Manaos, San Juan            </li>
<li>(UTC-04:00) Hora del Atlántico (Canadá)                     </li>
<li>(UTC-04:00) Santiago                                        </li>
<li>(UTC-03:30) Terranova                                       </li>
<li>(UTC-03:00) Brasilia                                        </li>
<li>(UTC-03:00) Buenos Aires                                    </li>
<li>(UTC-03:00) Cayena, Fortaleza                               </li>
<li>(UTC-03:00) Groenlandia                                     </li>
<li>(UTC-03:00) Montevideo                                      </li>
<li>(UTC-03:00) Salvador                                        </li>
<li>(UTC-02:00) Atlántico Central                               </li>
<li>(UTC-02:00) Hora universal coordinada-02                    </li>
<li>(UTC-01:00) Azores                                          </li>
<li>(UTC-01:00) Islas de Cabo Verde                             </li>
<li>(UTC) Casablanca                                            </li>
<li>(UTC) Dublín, Edimburgo, Lisboa, Londres                    </li>
<li>(UTC) Hora universal coordinada                             </li>
<li>(UTC) Monrovia, Reikiavik                                   </li>
<li>(UTC+01:00) Amsterdam, Berlín, Berna, Roma, Estocolmo, Viena</li>
<li>(UTC+01:00) Belgrado, Bratislava, Budapest, Liubliana, Praga</li>
<li>(UTC+01:00) Bruselas, Copenhague, Madrid, París             </li>
<li>(UTC+01:00) Sarajevo, Skopie, Varsovia, Zagreb              </li>
<li>(UTC+01:00) Windhoek                                        </li>
<li>(UTC+01:00) África Central Occidental                       </li>
<li>(UTC+02:00) Ammán                                           </li>
<li>(UTC+02:00) Atenas, Bucarest                                </li>
<li>(UTC+02:00) Beirut                                          </li>
<li>(UTC+02:00) Damasco                                         </li>
<li>(UTC+02:00) El Cairo                                        </li>
<li>(UTC+02:00) Estambul                                        </li>
<li>(UTC+02:00) Harare, Pretoria                                </li>
<li>(UTC+02:00) Helsinki, Kiev, Riga, Sofía, Tallin, Vilna      </li>
<li>(UTC+02:00) Jerusalén                                       </li>
<li>(UTC+02:00) Nicosia                                         </li>
<li>(UTC+03:00) Bagdad                                          </li>
<li>(UTC+03:00) Kaliningrado, Minsk                             </li>
<li>(UTC+03:00) Kuwait, Riad                                    </li>
<li>(UTC+03:00) Nairobi                                         </li>
<li>(UTC+03:30) Teherán                                         </li>
<li>(UTC+04:00) Abu Dabi, Muscat                                </li>
<li>(UTC+04:00) Bakú                                            </li>
<li>(UTC+04:00) Ereván                                          </li>
<li>(UTC+04:00) Moscú, S. Petersburgo, Volgogrado               </li>
<li>(UTC+04:00) Port Louis                                      </li>
<li>(UTC+04:00) Tiflis                                          </li>
<li>(UTC+04:30) Kabul                                           </li>
<li>(UTC+05:00) Islamabad, Karachi                              </li>
<li>(UTC+05:00) Tashkent                                        </li>
<li>(UTC+05:30) Chennai, Calcuta, Mumbai, Nueva Delhi           </li>
<li>(UTC+05:30) Sri Jayawardenepura                             </li>
<li>(UTC+05:45) Katmandú                                        </li>
<li>(UTC+06:00) Astana                                          </li>
<li>(UTC+06:00) Dacca                                           </li>
<li>(UTC+06:00) Ekaterimburgo                                   </li>
<li>(UTC+06:30) Rangún                                          </li>
<li>(UTC+07:00) Bangkok, Hanói, Yakarta                         </li>
<li>(UTC+07:00) Novosibirsk                                     </li>
<li>(UTC+08:00) Krasnoyarsk                                     </li>
<li>(UTC+08:00) Kuala Lumpur, Singapur                          </li>
<li>(UTC+08:00) Pekín, Chongqing, Hong Kong, Urumqi             </li>
<li>(UTC+08:00) Perth                                           </li>
<li>(UTC+08:00) Taipéi                                          </li>
<li>(UTC+08:00) Ulán Bator                                      </li>
<li>(UTC+09:00) Irkutsk                                         </li>
<li>(UTC+09:00) Osaka, Sapporo, Tokio                           </li>
<li>(UTC+09:00) Seúl                                            </li>
<li>(UTC+09:30) Adelaida                                        </li>
<li>(UTC+09:30) Darwin                                          </li>
<li>(UTC+10:00) Brisbane                                        </li>
<li>(UTC+10:00) Canberra, Melbourne, Sídney                     </li>
<li>(UTC+10:00) Guam, Port Moresby                              </li>
<li>(UTC+10:00) Hobart                                          </li>
<li>(UTC+10:00) Yakutsk                                         </li>
<li>(UTC+11:00) Islas Salomón, Nueva Caledonia                  </li>
<li>(UTC+11:00) Vladivostok                                     </li>
<li>(UTC+12:00) Auckland, Wellington                            </li>
<li>(UTC+12:00) Fiyi                                            </li>
<li>(UTC+12:00) Hora universal coordinada+12                    </li>
<li>(UTC+12:00) Magadán                                         </li>
<li>(UTC+12:00) Petropavlovsk-Kamchatsky - antiguo              </li>
<li>(UTC+13:00) Nuku'alofa                                      </li>
<li>(UTC+13:00) Samoa                                           </li>
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
### PLACES WALLET REST API

#### Get Places by Price
<p>Get Places that belongs to a root category or category that has no parent in a default radius of 3000mt or 3km</p>
<b>URL:</b> http://example.com/api/wallet/getplacesbetween?code={code}&latitude={latitude}&longitude={longitude}&minprice={minprice}&maxprice={maxprice} <br>
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
    <i>minprice(integer)</i> - the minimum price in the price filter
  </li>
  <li>
    <i>maxprice(integer)</i> - the maximum price in the price filter
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
status-code: 400 -> response-message: "formato_coordenadas_incorrecto"|"query_param_is_required"|"minprice_and_maxprice_required"
status-code: 500 -> response-message: "there_was_a_problemo_jefe"
```
