pipeline {
    agent any

    stages {
        stage('Build UI') { 
            steps {
                sh 'npm install' 
                sh 'webpack'
            }
        }
    }
}