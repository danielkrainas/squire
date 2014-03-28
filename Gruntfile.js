module.exports = function (grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        bump: {
            options: {
                files: ['package.json'],
                updateConfigs: ['pkg'],
                commit: true,
                commitMessage: 'release v%VERSION%',
                commitFiles: ['package.json', 'GlobalAssemblyInfo.cs'],
                createTag: true,
                tagName: 'v%VERSION%',
                tagMessage: 'Version v%VERSION%',
                push: false
            }
        },

        assemblyinfo: {
            options: {
                files: ['GlobalAssemblyInfo.cs'],
                info: {
                    version: function () {
                        return grunt.config('pkg.version');
                    }
                }
            }
        },

        mstest: {
            options: {

            }
        },

        mkdir: {
            all: {
                options: {
                    create: ['release']
                }
            }
        },

        msbuild: {
            dev: {
                src: grunt.file.expand({
                    filter: function (src) {
                        return !~src.indexOf(".Tests");
                    }
                }, 'src/**/*.csproj', 'tests/**/*.csproj'),
                options: {
                    projectConfiguration: 'Debug',
                    stdout: true,
                    targets: ['Clean', 'Rebuild'],
                    maxCpuCount: 4,
                    buildParameters: {
                        WarningLevel: 2
                    },

                    verbosity: 'quiet'
                }
            }
        },

        nugetpack: {
            dist: {
                src: 'src/*/*.csproj',
                dest: 'release',

                options: {
                    version: grunt.file.readJSON('package.json').version
                }
            }
        },

        nugetpush: {
            dist: {
                src: grunt.file.expand('release/*.nupkg'),
            },

            options: {
                apiKey: grunt.option('key')
            }
        }
    });

    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-nuget');
    grunt.loadNpmTasks('grunt-mkdir');
    grunt.loadNpmTasks('grunt-mstest');
    grunt.loadNpmTasks('grunt-dotnet-assembly-info');
    grunt.loadNpmTasks('grunt-bump');

    grunt.registerTask('default', ['mkdir', 'msbuild', 'mstest']);
    grunt.registerTask('test', ['mstest']);
    grunt.registerTask('publish', ['default', 'nugetpush']);
    grunt.registerTask('pack', ['mkdir', 'nugetpack']);
    grunt.registerTask('patch', ['bump-only:patch', 'assemblyinfo', 'msbuild', 'mstest', 'bump-commit']);
};