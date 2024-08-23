import React from 'react';
import { useEffect } from 'react';
import { useState } from 'react';
import BarChartCustom from '../../components/BarChartCustom';
import DashBoardCard from '../../components/DashBoardCard';
import PieChartSingle from '../../components/PieChartSingle';
import StackChart from '../../components/StackChart';
import TableStatistic from '../../components/TableStatistic';
import { instance } from '../../config/axiosConfig';
function limit(c) {
  return this.filter((x, i) => {
    if (i <= c - 1) {
      return true;
    }
  });
}
Array.prototype.limit = limit;
function addDays(date, days) {
  const dateCopy = new Date(date);
  dateCopy.setDate(date.getDate() - days);
  return dateCopy;
}
function addDays1(date, days) {
  const dateCopy = new Date(date);
  dateCopy.setDate(date.getDate() + days);
  return dateCopy;
}
const data = [
  {
    id: '111',
    name: 'Fuck',
    age: 18,
    count: 100,
    links: '',
  },
  {
    id: '111',
    name: 'Fuck',
    age: 18,
    count: 100,
    links: '',
  },
  {
    id: '111',
    name: 'Fuck',
    age: 18,
    count: 100,
    links: '',
  },
  {
    id: '111',
    name: 'Fuck',
    age: 18,
    count: 100,
    links: '',
  },
];
const lable = ['ID', 'Name', 'Description', 'User ID', 'More'];
const actionsUrl = {
  getPost: '/Statistics/getPostsBy',
  getPostByUser: '/Statistics/getPostsByUserIn',

};
const initDate = {

  start : addDays(new Date(),60).toISOString().slice(0,10),
  end:addDays1( new Date(),1).toISOString().slice(0,10)
}

function PostStatistic() {
  const [dataChar1, setDataChart1] = useState([]);
  const [dataTbale, setDataTable] = useState([]);
  const [timeSelect, setTimeSelect] = useState(3);
  const [timeOfStatistic,setTimeOfStatistic] = useState(1)
  const [typeOfStatistic,setTypeOfStatistic] = useState(1)
  const [dateFilter,setDateFilter] = useState(initDate)
  useEffect(() => {

    if(timeOfStatistic == 4){
      instance
      .put(`${actionsUrl.getPost}${typeOfStatistic == 1 ? 'Category' : "User"}${'InStages'}`,{
        "starDate": dateFilter.start,
        "endDate": dateFilter.end
      }).then(
        rs=>{
          if (typeOfStatistic ==2 ){
            let tempUser = new Array()
            rs.data.map(v => {
              if (!tempUser.includes(v.userId)){
                tempUser.push(v.userId)
              }
            })
            let dataChart = new Array()
            tempUser.map(v1 => {
              let count = 0;
              rs.data.map(v2 => {
                if (v2.userId == v1) count++;
              })
                dataChart.push({
                  name: rs.data.filter(x=>x.userId == v1)[0].user.userName,
                  totalPost: rs.data.length,
                  userPost: count,
                })
            })
            console.log(dataChart);
            setDataChart1(dataChart.sort((count1,count2) => count2 - count1).limit(5))
          }else{
            let tempUser = new Array()
            rs.data.map(v => {
              if (!tempUser.includes(v.categoryId)){
                tempUser.push(v.categoryId)
              }
            })
            let dataChart = new Array()
            tempUser.map(v1 => {
              let count = 0;
              rs.data.map(v2 => {
                if (v2.categoryId == v1) count++;
              })
                dataChart.push({
                  name: rs.data.filter(x=>x.categoryId == v1)[0].category.name,
                  totalPost: rs.data.length,
                  userPost: count,
                })
            })
            console.log(dataChart);
            setDataChart1(dataChart.sort((count1,count2) => count2 - count1).limit(5))
          }
        }
      )
    }
    else{

    instance
      .get(`${actionsUrl.getPost}${typeOfStatistic == 1 ? 'Category' : "User"}${timeOfStatistic == 1 ? 'InDay' : timeOfStatistic == 2 ? "InWeek" : "InMonth"}`)
      .then((rs) => {
        if (typeOfStatistic ==2 ){
        console.log(rs.data);
        let tempUser = new Array()
        rs.data.map(v => {
          if (!tempUser.includes(v.userId)){
            tempUser.push(v.userId)
          }
        })
        console.log(tempUser);
        let dataChart = new Array()
        tempUser.map(v1 => {
          let count = 0;
          rs.data.map(v2 => {
            if (v2.userId == v1) count++;
          })
            dataChart.push({
              name: rs.data.filter(x=>x.userId == v1)[0].user.userName,
              totalPost: rs.data.length,
              userPost: count,
            })
        })
        setDataChart1(dataChart.sort((count1,count2) => count2 - count1).limit(5))
      }
    else{
      let tempCategory = new Array()
        rs.data.map(v => {
          if (!tempCategory.includes(v.categoryId)){
            tempCategory.push(v.categoryId)
          }
        })
        console.log(tempCategory);
        let dataChart = new Array()
        tempCategory.map(v1 => {
          let count = 0;
          rs.data.map(v2 => {
            if (v2.categoryId == v1) count++;
          })
            dataChart.push({
              name: rs.data.filter(x=>x.categoryId == v1)[0].category.name,
              totalPost: rs.data.length,
              userPost: count,
            })
        })
        setDataChart1(dataChart.sort((count1,count2) => count2 - count1).limit(5))
    }
    }) }    
  }, [timeOfStatistic,typeOfStatistic,dateFilter]);

  useEffect(()=>{

    instance.get(timeSelect == 1 ? actionsUrl.getPostByUser+'Day' : timeSelect == 2 ? actionsUrl.getPostByUser+'Week' : actionsUrl.getPostByUser+'Month')
    .then (rs => {
        
        console.log(rs.data);
        let dataTemp = new Array()
        rs.data.map(({id,name,description,userId}) => {
    
          dataTemp.push({
            id,
            name,
            description,
            userId,
            more: '/product/'+ id
          })
        
        })

          setDataTable(dataTemp)
    })
  },[timeSelect])
  const handleChangeDateFilter = e =>{
        let {name,value} = e.target
        setDateFilter({
          ...dateFilter,
          [name]: value
        })
  }
  const handleChangeTimeOfStatistic = e => {
    setTimeOfStatistic(e.target.value)
  }
  const handleChangeTypeOfStatistic = e => {
    setTypeOfStatistic(e.target.value)
  }
  const setTime1 = (id) => setTimeSelect(id);
  return (
    <>
      <div className="select-part-statistic">
        <select
          className="select-1"
          name="typeOfStatistic"
          value={typeOfStatistic}
          onChange={handleChangeTypeOfStatistic}>
          <option value={1}>Category</option>
          <option value={2}>User</option>
        </select>
        <select
          className="select-2"
          name="timeOfStatistic"
          value={timeOfStatistic}
          onChange={handleChangeTimeOfStatistic}>
          <option value={1}>In Day</option>
          <option value={2}>In Week</option>
          <option value={3}>In Month</option>
          <option value={4}>In stage</option>
        </select>
        {timeOfStatistic == 4 &&<div className='date-filter'>{
      "From " }
        <input className='start-date' name='start' value={dateFilter.start} onChange={handleChangeDateFilter} type={'date'} />  {
      "To " }
        <input className='end-date' name='end' value={dateFilter.end} onChange={handleChangeDateFilter} type={'date'} /> 
    </div>}
      </div>
      <div className="chart-side">
        <div className="chart-side-1">
         { <StackChart nameChart={"Top 5 user"} data = {dataChar1}/> }
        </div>
        <div className="chart-side-2"></div>
        <TableStatistic
          name="New post"
          timeSelect={timeSelect}
          setTime={setTime1}
          data={dataTbale}
          lables={lable}
        />
      </div>
    </>
  );
}

export default PostStatistic;
