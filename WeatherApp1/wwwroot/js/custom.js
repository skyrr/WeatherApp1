﻿new Vue({
    el: '#app',
    data() {
        return {
            info: null
        };
    },
    mounted() {
        axios
            .get('http://localhost:51262/api/weather/forecast?city=6548737')
            .then(response => (this.info = response));
    }
});