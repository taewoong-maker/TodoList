<template>
    <select v-model="selected">
        <option v-for="data in selectList" :key="data" :value="data" selected="">{{data}}</option>
    </select>
</template>

<script>
export default {
    props: ['selectList', 'selectedValue'],
    data() {
        return {
            selected: this.selectedValue,
        }
    }
}
</script>
