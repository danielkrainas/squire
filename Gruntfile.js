module.exports = function (grunt) {

    grunt.initConfig({

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

    grunt.registerTask('default', ['mstest', 'mkdir', 'msbuild', 'nugetpack']);
    grunt.registerTask('test', ['mstest']);
    grunt.registerTask('publish', ['default', 'nugetpush']);
};