pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh 'dotnet build'
      }
    }

    stage('Test') {
      steps {
        sh 'DATABASE_URL="Server=172.31.8.135;Database=desafioqa;Uid=root;Pwd=root" dotnet test'
      }
    }

    stage('TestE2E') {
      steps {
        sh '''cd bdd && dotnet test'''
      }
    }

    stage('TestE3ECypress') {
      steps {
        sh '''cd bdd-cypress
npm test'''
      }
    }

  }
}
