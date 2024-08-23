import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import CardApprove from '../../components/CardApprove'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'
import CarouselImage from '../../components/CarouselImage'
const readOnly = [
  true,
  false,
  false,
  false,
  false
  
]
const lable = [
    "id","subject","body",'status',"reportedUserId","userId","reportCategoryId","createdDate","More"
  ]
const buttonActive = [true,false,false,true]
const types = [
  'string',
  'string',
  'string',
  'number',
  'string',
  'string',
  'number',
  'date',
  'more'
]
const initValues = {
  id : '',
  avatarUrl : '',
  username : "",
  email: '',
  
}


const actions = {
  getALl: '/Report/getAll',
  getRole: "/Roles/getRoleByUser",
  getById: "/Report/getById/",
  getProfile: '/user/profile',
  delete:'Product/delete/'
}

function CheckReport() {
  const [dataInfo,setDataInfo]  = useState([])
  const [infoActive,setInfoActive] = useState(false)
  const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [name,description,price,stock] = value
  const  valueAction = {name,description,price,stock}
  useEffect(()=> { 

      instance.get(actions.getALl)
      .then( rs => {
  
        let temp = new Array()
        
        rs.data.map(({id,subject,body,status,reportedUserId,reportedProductId,userId,reportCategoryId,createdDate}) => {
  
  
           temp.push({
            id,
            subject,
            body,
            status,
            reportedUserId,
            userId,
            reportedProductId,
            createdDate,
            reportCategoryId,
            more: id,
          })
        })
        setData(temp)
      })
      .catch(e => console.log(e))
    

  },[reload])


  const setReloaded = () => setReload(!reload)
  const setDataIF =  value => {
    setDataInfo(value)
      instance.get(`${actions.getById}${value}`)
      .then(  rs => {setDataInfo(rs.data)
      })
  }
 
 
  const setInfo = () => setInfoActive(!infoActive)
  return (
    <>
    <div className="search-table">
     {infoActive && <CardApprove setReloaded={setReloaded} productId = {dataInfo.reportedProductId} userId = {dataInfo.reportedUserId} report = {true} setInfo={setInfo} idInfo={dataInfo}>
      
     <CarouselImage report={true} idInfo  = {dataInfo}/>

      </CardApprove>}
      {/* <DateFilter />
      <CategoryFilter />
      <BrandFilter /> */}
    
      </div>
      <TableSearch lable={lable}   setDataInfo = {setDataIF} setInfo = {setInfo} buttonActive={buttonActive}  data={data} value={value} reload={reload} setReloaded={setReloaded} initValues={initValues} setValue={setValue} valueAction={valueAction}  actions = {actions} readOnly= {readOnly} types={types} />
    </>
  )
}

export default CheckReport
