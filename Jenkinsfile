pipeline {
    agent {
        docker {
            image 'ynfunc-docker.pkg.coding.net/easyrbac-api/easy-rbac/dotnetcoresdk:3.1'
            args '-v $HOME:/root'
        }
    }
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
                sh 'dotnet dotnet publish -r linux-x64 -c Release /p:PublishSingleFile=true'
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

                        sshPut remote: remote, from: 'easyRBAC/src/EasyRbac.Web/bin/Release/netcoreapp3.1/linux-x64/publish/EasyRbac.Web', into: '/home/function/tmp'
                        // sshCommand remote: remote, command: "tar -xzvf /home/function/tmp/easyRBAC.tar.gz -C /tmp/"
                        // sshCommand remote: remote, command: "systemctl --user stop easyrbac"
                        // sshCommand remote: remote, command: "cp -f -r /tmp/easyRBAC/src/EasyRbac.Web/bin/Release/netcoreapp2.2/linux-x64/publish/* /home/function/dotnet_apps/easyRBAC"
                        // sshCommand remote: remote, command: "systemctl --user start easyrbac"
                        // sshRemove remote: remote, path: '/tmp/easyRBAC'
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