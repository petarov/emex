.o: EmEx :o.
==========================


# Important notice
 
 * Current version is just for **demo** purposes. Version is tested only on Win32 platforms.
 * Indexing LARGE IMAP boxes will take *too much* time or may **not work** at all!
 * GMail is not supported (even though it's in the setup) - don't use it ;)

# Requirements before running

 1. Win32 System -  The Frontend client requires .NET Framework 3.5 installed to run. 
                    (This should be available anyway with Windows XP SP3!)
 2. Strawberry Perl installed (http://strawberryperl.com/)
 3. Have OpenSSL installed for IMAPS connections (check Install & Running section)
 4. Required Perl modules must be installed (check Install & Running section) 
 
 
 # Install & Running
 
 Follow these steps in order to build and run the project.

 1. Have Strawberry Perl installed. (http://strawberryperl.com/) 
 2. Have OpenSSL installed. (http://www.slproweb.com/products/Win32OpenSSL.html)
    NOTE: After you install OpenSSL copy C:\OpenSSL\lib\MinGW files to C:\OpenSSL\lib\ 
 3. Go to scripts/ folder and start install-required-perl-modules.bat script in order to install required Perl modules.
 4. Extract the emex zip archive in some filder i.e. c:\emex-full.
 5. Start the project using the emex.bat script !
 6. Use credentials "emex/emex" (without quotes) for the Backend username and password paremters.
  
 # TroubleShooting
 
*(Log files are placed in x:\emex-install-path\data\logs folder)*
 
### PROBLEM: No connection could be made because the target machine actively refused it 127.0.0.1:8080

* Check if you have webservice (i.e. Apache) running on localhost and port 8080

* Check if all Perl modules are installed - open a console and go to the EmEx install folder, Type:
    "perl backend/bootstrap.pl"
If there are no errors, then Perl is OK!
      
* Should you get an error message when installing module "Net::SSLeay" about missing libssl32.dll functions (EVP_CIPHER_iv_ or sthg. like that), then 
you probably have older SSL libs installed in \windows\system32 !
      
      
     
 
