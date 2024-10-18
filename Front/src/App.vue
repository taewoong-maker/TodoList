<template>
  <div id="nav">
    <router-link to="/calendar">Calendar</router-link>
  </div>
  <router-view/>
</template>
<script>
  export default {
    // 이건 dev에서 커밋한거야1111
    // 이건 dev에서 커밋한거야1222
    // 이건 dev에서 커밋한거야1333
  }
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
