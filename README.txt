README
======
To provide some means to manage our DB2 for z/OS services, we need an application that can:

1. Connect to DB2 and itemise the services that we are allowed to see
2. Create new services
3. Drop services
4. Display information about a service

To support all of this, we will need to be able to connect and authenticate in one of three modes:

1. HTTP Basic (base64 encoded) userid and password
2. HTTPS Basic (base64 encoded) userid and password
3. HTTPS client certificate authentication

We can now enumerate the certificates that include the private as well as public keys (required for client cert auth), so what do we need to do next?

a) *done* Perform HTTP GET with Basic userid and password
b) *done* Perform HTTP POST with Basic userid and password and JSON parameter data
c) *done* Perform HTTPS GET with Basic userid and password
d) *done* Perform HTTPS POST with Basic userid and password and JSON parameter data
e) *done* Perform HTTPS GET with client certificate
f) *done* Perform HTTPS POST with client certificate and JSON parameter data
g) *done* Handle bad status from DB2
h) *done* Handle self-signed certificate errors
i) *done* Use certificate store client certificate

App will need to:

* On startup
	select host (allow defaulted value)
	select protocol (HTTP / HTTPS)
	select userid/pwd or certificate
	then show list of services
* Manage resizing - and save sizes in the registry
* Manage position on screen and save this in the registry

105,160,305,410
1021


Registry entries - under wDB2REST:

Config
	LHOST - Last (AKA) host used

Hosts
	AKA  - string with the name that will appear in the list box
	IP   - IP address / name
	PORT - port number to use
	SEC  - bool = true if HTTPS
	UCRT - (if SEC) and this is true, use certificate
	USER - (if !SEC v !UCRT) string = userid for BASIC auth
	PWD  - (if !SEC v !UCRT) string = password for USER
	CFLE - (if SEC & UCRT) bool - true = CERT = file name for cert, else Subject name
	CERT - (if SEC & UCRT) string - if CFLE then file name for cert, else Subject name for cert in store