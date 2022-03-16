<template>
    <div id="">
        <div id="TodoTitle">
            <span id="TodoTitleSpan">
                오늘 할 일
            </span>
        </div>
        <div id="TodoItems" v-for="(item, index) in TodaysTodoList" :key="index">
            <div :class="item.Finished ? finished : unFinished">
                <span>{{item.TodoDetail}}</span>
                <button :data-index="item.index" @click.prevent="fnClickFinishBtn(item.Number)">
                    완료 </button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: ['TodaysTodoList'],
    data () {
        return {
            finished: 'finished TodoItem',
            unFinished: 'unFinished TodoItem'
        }
    },
    methods: {
        fnClickFinishBtn(index) {
            alert(index);
        }
    }
}
</script>

<style>
    #TodoTitle{
        margin: 5px 0;
    }
    #TodoItems{
        display: flex;
        flex-flow: nowrap column;
    }
    .TodoItem{
        background: rgb(226, 226, 226);
        border-radius: 10px;
        margin: 5px;
        font-size: 20px;
    }
</style>
