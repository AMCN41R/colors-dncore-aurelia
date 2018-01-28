pipeline {
  agent any
  stages {
    stage('Build UI') {
      parallel {
        stage('Build UI') {
          steps {
            sh 'npm install'
            sh 'webpack'
          }
        }
        stage('Build C#') {
          steps {
            sh 'dotnet build'
          }
        }
      }
    }
  }
}