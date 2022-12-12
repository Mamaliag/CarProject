<template>
<div id="MyCarsPage">
<my-cars/>
</div>
</template>

<script>


import MyCars from '../components/MyCars.vue';

export default {
  name: "Home",
  components: {
    MyCars,
  },
};
</script>
