pipeline {
    agent any
    stages {
        stage('检出') {
            steps {
                checkout([$class           : 'GitSCM', branches: [[name: env.GIT_BUILD_REF]],
                          userRemoteConfigs: [[url: env.GIT_REPO_URL, credentialsId: env.CREDENTIALS_ID]]])
            }
        }
        stage('构建') {
            steps {
                echo '构建中...'
                sh './.github/workflows/build.sh'
                echo 'build完成'               
                archiveArtifacts(artifacts: 'easyRBAC.tar.gz', fingerprint: true, onlyIfSuccessful: true)
            }
        }
        stage('部署') {
            steps {
                echo '部署中...'
                script {
                    def remote = [:]
                    remote.name = 'Function-App01'
                    remote.allowAnyHosts = true
                    remote.host = '47.113.105.175'
                    remote.user = 'function'

                    // 需要先创建一对 SSH 密钥，把私钥放在 CODING 凭据管理，把公钥放在服务器的 `.ssh/authorized_keys`，实现免密码登录
                    withCredentials([sshUserPrivateKey(credentialsId: "79923990-f24a-4e30-a81d-9e64811b6603", keyFileVariable: 'id_rsa')]) {
                        remote.identityFile = id_rsa

                        sshPut remote: remote, from: 'easyRBAC.tar.gz', into: '/home/function/tmp'
                        // sshCommand remote: remote, command: "systemctl --user stop easyrbac"
                        // sshCommand remote: remote, command: "cp -f /home/function/java_apps/xin-an-api/api.jar /home/function/java_apps/xin-an-api/api.jar.bak"
                        // sshCommand remote: remote, command: "cp -f /home/function/tmp/api.jar /home/function/java_apps/xin-an-api/"
                        // sshCommand remote: remote, command: "systemctl --user start xinan-api"
                    }
                }

                echo '部署完成'
            }
        }
    }
}