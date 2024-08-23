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
  true,
  true,
  true
]
  

const types = [
  'string',
  'string',
  'number',
  'string',
  'number',
  'string',
  'number',
  'date',
  'date'
]
const initValues = {
  id : '',
  packageName : '',
  packageValue : 0,
  valueUnit: '',
  coinAmount: 0,
  description: '',
  status: 0,
  createdDate: '',
  updatedDate: ''
}
const lable = [
  "ID","Package name","Package value",'Value unit','Coin amount',"Description", "Status","Create date","Update date"
]

const actions = {
  getALl: '/CoinPackage/getALl',
  put : '/CoinPackage/update/',
  post: '/CoinPackage/add',
  delete: '/CoinPackage/delete/'
}
function CoinPackage() {
  const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [id,packageName,packageValue,valueUnit,coinAmount,description] = value
  const  valueAction = {id,packageName,packageValue,valueUnit,coinAmount,description}
  useEffect(()=> { 
    
    instance.get(actions.getALl)
    .then( rs => {

      let temp = new Array()

      rs.data.map(({id,packageName,packageValue,valueUnit,coinAmount,description,status,createdDate,updatedDate}) => {
        
        temp.push({
          id,
          packageName,
          packageValue,
          valueUnit,
          coinAmount,
          description,
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

export default CoinPackage
