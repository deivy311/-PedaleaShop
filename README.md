
# Pedalea Shop
## Requirementes
```
.Net = 6
Microsoft Sql server
Microsoft Sql Server Managment Studio
```

## Install Required tools and packages.


### Cloning Repositories
#### WebApp and the submodules dependecies like the web.Api for this it must be cloned using  the atribute _recursive_
```
git clone --recursive git@github.com:deivy311/PedaleaShop.git

```
## Verifiying the WebApi complete cloning
In case in the path _PedaleaShop\Dependencies\PedaleaShop.WebApi_ there is empty it should be ran the following command
```
git clone git@github.com:deivy311/PedaleaShop.WebApi.git
```

## DataBase back up recovering
To get the respective PEdaleaDB it is needed open the file _PedaleaDB.sql_ (it should be lcoated in the PEdaleaShop repository in the folder _PedaleaShop\SQLScripts_) with _SQL Server Managment Studio_ and following the next steps
 1. Double click in the file _PedaleaDB.sql_ or open it with _SQL Server Managment Studio_.
 2. Run the script, it should be enough to recover the PedaleaDB.


### Running the WebApi 
1. This step must be ran at first, the application is located in the dependecies folder in the following path _PedaleaShop\Dependencies\PedaleaShop.WebApi/PedaleaShop.WebApi.sln_
2. the next step is to run the application, by pressing _F5_.

### running the WebApp Pedalea Shop
1. This step must be ran at second just once the WebApi is already running. The application is located in the Root folder _PedaleaShop/PedaleaShop.sln_
2. the next step is to run the application, by pressing _F5_.

## Evaluation
This should be enough to get the complete applciation working, by default there is an authenticated user with the following credentials:
* UserName: davidestebanimbajoa@gmail.com
* Password: UserTest1,.

Thanks! 