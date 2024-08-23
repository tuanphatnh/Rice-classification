import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import BarChartCustom from '../../components/BarChartCustom'
import DashBoardCard from '../../components/DashBoardCard'
import PieChartSingle from '../../components/PieChartSingle'
import TableStatistic from '../../components/TableStatistic'
import { instance } from '../../config/axiosConfig'
const data = [{
  id: '111',
  name: "Fuck",
  age: 18,
  count: 100,
  links: ''
},
{
  id: '111',
  name: "Fuck",
  age: 18,
  count: 100,
  links: ''
},
{
  id: '111',
  name: "Fuck",
  age: 18,
  count: 100,
  links: ''
},
{
  id: '111',
  name: "Fuck",
  age: 18,
  count: 100,
  links: ''
}]
const lable = ["ID",'Name','Description',"User ID", 'More']
const actionsUrl = {
  getPost: '/Statistics/getPostsByCategoryInMonth',
  getUser: 'Statistics/monthly-registers?year=',
  getReport: '/Statistics/monthly-reports?year=',
  getPostByUser: '/Statistics/getPostsByUserIn',


}
const monthNames = ["","January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];

let innitMonthData = [0,0,0,0,0,0,0,0,0,0,0,0]
function DashBoard() {
    const [dataChar1,setDataChart1] = useState([])
    const [dataTbale,setDataTable] = useState([])
    const [timeSelect,setTimeSelect] = useState(3)
    useEffect(()=>{
        instance.get(`${actionsUrl.getUser}${new Date().getFullYear()}`)
        .then( rs => {
         rs.data.map(v =>{
          innitMonthData[v.month] = v.numberOfNewUser
         })
          let data = new Array()
          for (let index = 1; index <= 12; index++) {
            let temp = {
              name: monthNames[index],
              uv: innitMonthData[index],
           
            }
            data.push(temp)
          }
          setDataChart1(data);
        })
       
    }
    ,[])

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
    const setTime1 = (id) => setTimeSelect(id)
  return (
    <>
    <DashBoardCard actionsUrl = {actionsUrl} />
   <div className='chart-side'>
    <div className='chart-side-1'>
    <BarChartCustom nameChart='New Register' data ={dataChar1} />
    </div>
    <div className='chart-side-2'>
    </div>
    <TableStatistic name='New post' timeSelect = {timeSelect} setTime={setTime1} data={dataTbale} lables = {lable} />

   </div>
   </>
  )
}

export default DashBoard
