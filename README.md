1. Udhëzimet për ekzekutimin e programit:
Ekzekutimi i programit bëhet prej Command Prompt duke i dhënë vlerat e argumenteve përkatëse. Varësisht prej vlerave të argumenteve që i japim, e thirrim komandën dhe nënkomandën përkatëse.




2. Përshkrim i shkurtër për komandat:





Komanda rail-fence

Për këtë komandë jemi bazuar në disa linqe me tutoriale të ndryshëm një ndër to edhe http://practicalcryptography.com/ciphers/rail-fence-cipher/ .
Rail-fence cipher është shumë i lehtë për tu zbuluar. Celësi i saj është numri i shiritave (rails). Ajo punon në këtë parim:"shkruaj shkronjat përgjatë kolonave, lexo përgjatë rreshtave". Kodin për këtë komandë e kemi shkruar duke u bazuar në modelin që kemi hasur në  këtë link dhe shumicën e literaturave.

P.sh. për plaintext-in defend the east wall of the castle, për celësin 3, kemi ciphertext-in dnetlhseedheswloteateftaafcl, ku hapësirat nuk i kemi përfshirë në enkriptim.

Nënkomanda encrypt

Me anë të kësaj nënkomande bëhet enkriptimi i plaintext-it në ciphertext. Në programin tonë kjo komandë është realizuar përmes një metode që ka dy parametra: plaintext-in dhe numrin e shiritave.

Nënkomanda decrypt

Me anë të kësaj nënkomande bëhet dekriptimi i ciphertext-it në plaintext. Në programin tonë kjo komandë është realizuar përmes një metode që ka dy parametra: ciphertext-in dhe numrin e shiritave.

3. Shembuj të ekzekutimit:





Komanda rail-fence

Nënkomanda encrypt

Microsoft Windows [Version 10.0.18362.535]
(c) 2019 Microsoft Corporation. All rights reserved.

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence encrypt 3 "takohemi neser"

thnraoeieekms

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence encrypt 5 "departamenti i kompjuterikes"
depeemnmjkspatouiatiktrrie

Nënkomanda decrypt

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence decrypt 3 "thnraoeieekms"

takohemineser

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence decrypt 5 "depeemnmjkspatouiatiktrrie"
departamentiikompjuterikes
