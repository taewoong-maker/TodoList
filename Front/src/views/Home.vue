<template>
  <div class="home">
    <div class="container">
      <div class="item item1">1</div>
      <div class="item item2">2</div>
      <div class="item item3">3</div>
      <div class="item item4">4</div>
      <div class="item item5">5</div>
      <div class="item item6">6</div>
      <div class="item item7">7</div>
      <div class="item item8">8</div>
      <div class="item item9">9</div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: 'Home',
  components: {
  }
}
</script>

<style>
  .container{
    height: 80vh;
    background: beige;
    display: flex;
    flex-flow: nowrap row;
    justify-content: center;
  }
  .item{
    width: 100px;
    height: 100px;;
  }
  .item1{ background: red;}
  .item2{ background: orange;}
  .item3{ background: yellow;}
  .item4{ background: yellowgreen;}
  .item5{ background: skyblue;}
  .item6{ background: #b39ddb;}
  .item7{ background: #e3a1f0;}
  .item8{ background: #ef9a9a;}
  .item9{ background: #f48fb1;}
</style>
