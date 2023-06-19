## Project name: Pharmacy Company
### Description: Console application for accounting of goods in pharmacies.
---

The application menu is displayed on the main screen. To enter the submenu, press the corresponding key.
<br/>
<div align="center"><img src="https://github.com/de4rbe4r/PharmacyCompany/blob/master/Images/1.png" width="800"/></div>
</br>
Each submenu displays relevant information about the product, pharmacy, storage, batch. Each submenu has the ability to add a new entity and delete an existing one.
</br>
<div align="center"><img src="https://github.com/de4rbe4r/PharmacyCompany/blob/master/Images/2.png" width="700"/></div>
<div align="center"><img src="https://github.com/de4rbe4r/PharmacyCompany/blob/master/Images/3.png" width="800"/></div>
<div align="center"><img src="https://github.com/de4rbe4r/PharmacyCompany/blob/master/Images/44.png" width="800"/></div>
</br>
Each batch stores information on the quantity of the product in the batch and its location in a specific storage. For the pharmacies submenu, there is an additional option to display a list of products related to this pharmacy and their quantity in all pharmacy warehouses.
</br>
<div align="center"><img src="https://github.com/de4rbe4r/PharmacyCompany/blob/master/Images/6.png" width="800"/></div>
</br>
When deleting any of the objects, dependent objects are deleted, for example, when deleting a pharmacy with id = 1, all storages associated with this pharmacy are deleted, and all batches associated with the storages being deleted are deleted.
</br>
<div align="center"><img src="https://github.com/de4rbe4r/PharmacyCompany/blob/master/Images/7.png" width="800"/></div>

#### Language and tools:
* C#
* Visual Studio
* .NET Framework
* T-SQL
* MS SQL

---
P.S. To create a database and fill it with initial data, you need to uncomment the lines in the Program.cs file
