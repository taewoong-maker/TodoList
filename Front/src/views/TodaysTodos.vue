<template>
    <div id="">
        <div id="TodoTitle">
            <span id="TodoTitleSpan">
                오늘 할 일
            </span>

        </div>
        <div id="TodoItems" >
            <!-- <div :class="item.Finished ? finished : unFinished">
                <span>{{item.TodoDetail}}</span>
            </div> -->
            <el-checkbox v-for="(item, index) in TodaysTodoList"
                v-model="item.Finished" @click.prevent="fnChangeFinished(index)" :key="index" :label="item.TodoDetail" size="large" border />
        </div>
    </div>
</template>

<script>
export default {
    props: ['TodaysTodoList'],
    data () {
        return {
            todaysTodoList: this.TodaysTodoList
        }
    },
    methods: {
        fnChangeFinished: function(index) {
            this.todaysTodoList[index].Finished = !this.todaysTodoList[index].Finished;
        }
    },
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
        border-radius: 10px;
        font-size: 20px;
        margin: 5px;
    }
    .el-checkbox:last-of-type {
        margin-right: none;
    }
</style>
