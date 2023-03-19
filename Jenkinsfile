pipeline {
  agent any
  stages {
    
    stage('Build') {
      steps {
        sh '''
            dotnet build
        '''
      }
    }

    stage('Test') {
      steps {
        sh '''
            export DATABASE_URL="Server=172.31.6.94;Database=desafioqa;Uid=root;Pwd=root"
            dotnet test
        '''
      }
    }

    stage('TestE2E') {
      steps {
        sh '''
            export DATABASE_URL="Server=172.31.6.94;Database=desafioqa;Uid=root;Pwd=root"
            cd  ui 
            dotnet run --urls=http://localhost:5001 &
            cd ../bdd 
            
            export DRIVER_PATH="/usr/bin/firefox"
            export HOST="http://localhost:5001"
            
            dotnet test
        '''
      }
    }

    stage('Deploy Dev') {
      steps {
        sh '''
            echo "Aqui colocar o deploy"
            # ansible-plabook ...
        '''
      }
    }

    stage('Deploy Stage') {
      steps {
        sh '''
            echo "Aqui colocar o deploy"
            # ansible-plabook ...
        '''
      }
    }

    stage('Deploy Prod') {
      steps {
        sh '''
            echo "Aqui colocar o deploy"
            # ansible-plabook ...
        '''
      }
    }

  }
}
