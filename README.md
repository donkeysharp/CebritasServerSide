CebritasServerSide
==================

Server side services for Cebritas project, this will contain REST API for consume from mobile devices.

It will also have the monitor web site in which problems will be shown and official users can report a problem.
By official users we understand:
 * Media
 * Police
 * Town Hall

It will contain an internal embedded job(Quartz.Net) that will delete past problems, etc.


### REST API

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
    <i>longitude(double)</i> - latitude from where the problem has been reported, it has the same latitude's format
  </li>
  <li>
    <i>description(string)</i> - it's the problems description described by the user
  </li>
  <li>
    <i>type(integer)</i> - it's the type of the problem. This number has the next "meaning" mapping:
    <ul>
      <li>
        1 - Traffic problem
      </li>
      <li>
        2 - 
      </li>
    </ol>
  </li> 
</ul>
