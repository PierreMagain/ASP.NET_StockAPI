## Base de données pour l'API

Cette API nécessite une base de données pour fonctionner. Vous pouvez facilement démarrer une instance de la base de données en suivant ces étapes :

1. **Récupérez l'image de la base de données à partir de mon Docker Hub** :
   ```bash
   docker pull pierremagain/sql_server:latest
   
2. **Lancez le conteneur de la base de données** :
   ```bash
   docker run -d -p 1433:1433 --name ma_base_de_donnees pierremagain/sql_server:latest

3. **La base de données est maintenant accessible** :
  Une fois que le conteneur est en cours d'exécution, vous pouvez accéder à la base de données à l'adresse suivante :
  
      Adresse : localhost:1433
      Nom d'utilisateur : SA (par défaut)
      Mot de passe : 123456a. (par défaut)

   
Vous pouvez maintenant utiliser cette base de données avec l'API.
