# Insurance Records Web application  

***English text follows***

**Instrukce:**

Před prvním spuštěním aplikace, otevřete **Package Manager Console** a aktualizujte databázi příkazem: "update-database".  
Tímto budou vytvořeny dvě uživatelské role (User a Administrator) a jeden účet administrátora.

**Přihlašovací údaje administrátora:**  
**Email:** admin@test.com  
**Heslo:** Admin!123

**Aplikace:**
* obsahuje kompletní správu (CRUD) pojištěných a jejich pojištění (k přístupu ke správě musí být uživatel přihlášen)
    * Vytvoření pojištěného
    * Vytvoření pojištění
    * Zobrazení detailu pojištěného včetně jeho pojištění
    * Zobrazení detailu pojištění
    * Zobrazení seznamu pojištěných
    * Odstranění pojištěného včetně všech jeho pojištění
    * Odstranění pojištění
    * Editace pojištěného
    * Editace pojištění pojištěného
    * Dané entity jsou uloženy v SQL databázi
* eviduje pojistné události pojištěných, rovněž pomocí kompletní správy (CRUD)
* podporuje registraci a přihlášení uživatele
    * Editace uživatele
    * Změna hesla
* podporuje uživatelské role (User, Administrator)
    * Administrator má plný přístup k datům a může je měnit
* je plně responzivní
* generuje statistiky uživatele ve fromě PDF souboru

<br/>

***English version***

**Instructions:**

Before the first start of the application, open **Package Manager Console** and update the database by typing: "update-database".  
Two roles (User and Administrator) and one Administrator account will be created upon this initialization.

**Login for Administrator:**  
**Email:** admin@test.com  
**Password:** Admin!123

**The application:**
* contains the complete administration (CRUD) of the insured and their insurance (user must be logged in in order to have access to the CRUD)
    * Creation of the insured
    * Creation of the insurance
    * Detail view of the insured and his insurances
    * Detail view of the insurance
    * List view of insured
    * Deletion of the insured, including all his insurances
    * Deletion of the insurance
    * Edit of the insured
    * Edit of the insurance            
    * Entities are stored in the SQL database
* contains the complete administration (CRUD) of the insurance events
* supports signup and login feature
    * Edit of the user
    * Password change
* supports user roles (User, Administrator)
    * Administrator has full access to data and can edit the data
* is fully responsive
* generates user reports as PDF file     
