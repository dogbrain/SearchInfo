<template>
    <div class="main">
        <h1>Search Results</h1>
        <div class="search">
            <input v-model="searchString" placeholder="Enter search terms"/>
            <select v-model="selected">
                <option value="0" >Single back-end call</option>
                <option value="1">Multiple back-end calls</option>
            </select>
            <button v-on:click="onSearchClick()" :disabled='disableSearchButton'>Search</button>
        </div>
        <div class="searchResults">
            <p>Result:</p>
            <ul>
                <li v-for="(result, index) in searchResult" :key="index">
                    {{ result }}
                </li>
            </ul>
        </div>
        <div class="providers">
            <p>Providers:</p>
            <ul>
                <li v-for="(provider, index) in providers" :key="index">
                    {{ provider }}
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                providers: ['None - start api and reload?'],
                searchString: '',
                selected: '0',
                searchResult: null
            }
        },
        mounted() {
            this.loadProviders();
        },
        computed: {
            disableSearchButton: function () {
                return this.searchString.trim().length < 1;
            }
        },
        methods: {
            onSearchClick: function () {
                this.searchResult = [];
                if (this.selected == '0') {
                    axios
                        .get('https://localhost:5001/Search?query=' + this.searchString)
                        .then(response => {
                            (this.searchResult = response.data)
                        });
                } else {
                    var singleSearchTerms = this.searchString.split(' ').map(s => s.trim());
                    this.providers.forEach(provider => {
                        var item = { "provider": provider, "numberOfResults": 0 };
                        
                        this.searchResult.push(item);
                        singleSearchTerms.forEach(term => {
                            axios
                                .get('https://localhost:5001/Search?query=' + term + '&providers=' + provider)
                                .then(response => {
                                    debugger; // eslint-disable-line no-debugger
                                    item.numberOfResults += response.data[0].numberOfResults;
                                });
                        });
                        singleSearchTerms.sort()
                    });
                }
            },
            loadProviders: function () {
                axios
                    .get('https://localhost:5001/Search/providers')
                    .then(response => {
                        (this.providers = response.data)
                    });
            }
            
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
