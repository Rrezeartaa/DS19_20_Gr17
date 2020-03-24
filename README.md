1. Udhëzimet për ekzekutimin e programit:
Ekzekutimi i programit bëhet prej Command Prompt duke i dhënë vlerat e argumenteve përkatëse. Varësisht prej vlerave të argumenteve që i japim, e thirrim komandën dhe nënkomandën përkatëse. Në qoftë se jepni numër të ndryshëm të argumenteve, atëherë do t'ju paraqiten Exceptions.

2. Përshkrim i shkurtër për komandat:





Komanda rail-fence

Për këtë komandë jemi bazuar në disa linqe me tutoriale të ndryshëm një ndër to edhe http://practicalcryptography.com/ciphers/rail-fence-cipher/ .
Rail-fence cipher është shumë i lehtë për tu zbuluar. Celësi i saj është numri i shiritave (rails). Ajo punon në këtë parim:"shkruaj shkronjat përgjatë kolonave, lexo përgjatë rreshtave". Kodin për këtë komandë e kemi shkruar duke u bazuar në modelin që kemi hasur në  këtë link dhe shumicën e literaturave.

Nënkomanda encrypt

Me anë të kësaj nënkomande bëhet enkriptimi i plaintext-it në ciphertext. Në programin tonë kjo komandë është realizuar përmes një metode që ka dy parametra: plaintext-in dhe numrin e shiritave.

Nënkomanda decrypt

Me anë të kësaj nënkomande bëhet dekriptimi i ciphertext-it në plaintext. Në programin tonë kjo komandë është realizuar përmes një metode që ka dy parametra: ciphertext-in dhe numrin e shiritave.

Komanda Numerical
Përmes kësaj komande është mundësuar zevendësimi i secilës shkronjë me pozitën e tyre në alfabet dhe anasjelltas.
Kemi arritur që shkronjën a të zevëndesojmë me numërin 1 b=2,c=3 etj duke u bazuar dhe merre informacione ne këto resurse burimore:
*********


Nenkomanda Encode

Enkodon tekstin <text> në pozitat alfabetike të shkronjave.


Nenkomanda Decode

Dekodon vargun <code> nga shifrat në shkronjat përkatëse.

3. Shembuj të ekzekutimit:





Komanda rail-fence

Nënkomanda encrypt

Microsoft Windows [Version 10.0.18362.535]
(c) 2019 Microsoft Corporation. All rights reserved.

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence encrypt 3 "takohemi neser"

th eaoeinsrkme

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence encrypt 5 "departamenti i kompjuterikes"

deoiemnkmrkpat peeatiijtsr u

Nënkomanda decrypt

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence decrypt 3 "th eaoeinsrkme"

takohemi neser

C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds rail-fence decrypt 5 "deoiemnkmrkpat peeatiijtsr u"
departamenti i kompjuterikes

Nenkomanda Encode
C:\Users\Admin\source\repos\ds\ds\bin\Debug>ds numerical encode "takohemi neser"

20 1 11 15 8 5 13 9 14 5 19 5 18
