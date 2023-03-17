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
        sh 'dotnet test'
      }
    }

    stage('TestE2E') {
      steps {
        sh '''cd bdd
dotnet test'''
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