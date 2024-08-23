import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import CarouselImage from '../../components/CarouselImage'
import FileReview from '../../components/FileReview'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'
const readOnly = [
  true,
  false,
  false,
  false,
  false,
  false,
  false,
  true,
  true
]
  

const types = [
  'number',
  'string',
  'string',
  'number',
  'number',
  'number',
  'number',
  'date',
  'date'
]
const initValues = {
  id : 0,
  packageName : '',
  packageValue : '',
  valueUnit: 0,
  coinAmount: 0,
  description: 0,
  status: 0,
  createdDate: '',
  updatedDate: ''
}
const lable = [
  "ID","Name","Description","Price","Post Amout","Expired In", "Status","Create date","Update date"
]

const actions = {
  getALl: '/SubscriptionPackage/getALl',
  put : '/SubscriptionPackage/update/',
  post: '/SubscriptionPackage/add',
  delete: '/SubscriptionPackage/delete/'
}
function SubcriptionPackage() {
  const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [id,name,description,price,postAmout,expiredIn] = value
  const  valueAction = {id,name,description,price,postAmout,expiredIn}
  useEffect(()=> { 
    
    instance.get(actions.getALl)
    .then( rs => {

      let temp = new Array()

      rs.data.map(({id,name,description,price,postAmout,expiredIn,status,createdDate,updatedDate}) => {
        
        temp.push({
          id,
          name,
          description,
          price,
          postAmout,
          expiredIn,
          status,
          createdDate,
          updatedDate
        })
      
      })
      setData(temp)
    })
    .catch(e => console.log(e))

  },[reload])
  const setReloaded = () => setReload(!reload)
  

return (
 <TableSearch lable={lable}  data={data} value={value} reload={reload} setReloaded={setReloaded} initValues={initValues} setValue={setValue} valueAction={valueAction}  actions = {actions} readOnly= {readOnly} types={types} />
)
}

export default SubcriptionPackage
