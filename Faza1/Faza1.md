Udhëzimet për ekzekutimin e programit: 

Ekzekutimi i programit bëhet prej Command Prompt duke i dhënë vlerat e argumenteve përkatëse.
Varësisht prej vlerave të argumenteve që i japim, e thirrim komandën dhe nënkomandën përkatëse. Në qoftë se jepni numër të ndryshëm 
të argumenteve, atëherë do t'ju paraqiten Exceptions.

Përshkrim i shkurtër për komandat:
Komanda count
Komanda count përdoret për të numëruar elementët e një teksti ose vetitë e një objekti.

Komanda Numerical 

Përmes kësaj komande është mundësuar zevendësimi i secilës shkronjë me pozitën e tyre në alfabet dhe anasjelltas. Kemi arritur që 
shkronjën a të zevëndesojmë me numërin 1 b=2,c=3 etj duke u bazuar dhe merre informacione ne këtë resurse burimore:
https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-encoding

Nenkomanda Encode

Enkodon tekstin në pozitat alfabetike të shkronjave.

Nenkomanda Decode

Dekodon vargun nga shifrat në shkronjat përkatëse.

Komanda rail-fence

Për këtë komandë jemi bazuar në disa linqe me tutoriale të ndryshme: http://practicalcryptography.com/ciphers/rail-fence-cipher.
Rail-fence cipher është shumë i lehtë për tu zbuluar. Celësi i saj është numri i shiritave (rails). Ajo punon në këtë parim:"shkruaj 
shkronjat përgjatë kolonave, lexo përgjatë rreshtave". Kodin për këtë komandë e kemi shkruar duke u bazuar në modelin që kemi hasur në 
këtë link dhe shumicën e literaturave.

Nënkomanda encrypt

Me anë të kësaj nënkomande bëhet enkriptimi i plaintext-it në ciphertext. Në programin tonë kjo komandë është realizuar përmes një metode
që ka dy parametra: plaintext-in dhe numrin e shiritave.

Nënkomanda decrypt

Me anë të kësaj nënkomande bëhet dekriptimi i ciphertext-it në plaintext. Në programin tonë kjo komandë është realizuar përmes një metode
që ka dy parametra: ciphertext-in dhe numrin e shiritave.

Nënkomanda show

Kjo nënkomandë paraqet tekstin e organizuar në shirita (në formë valore) sipas së cilës bëhet edhe enkriptimi i tekstit. Për këtë jemi
bazuar në këtë link: https://www.geeksforgeeks.org/print-string-wave-pattern/
