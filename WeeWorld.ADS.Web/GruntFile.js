module.exports = function (grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        sass: {
            dist: {
                files: {
                    'content/styles/app2.css': 'content/styles/app2.scss'
                },
                options: {
                    style: 'expanded'
                },
            }
        },

        watch: {
            scripts: {
                files: [
                    '**/*.scss'
                ],
                tasks: ['sass']
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.registerTask('default', ['watch']);
}
