
const wordsBan  =  ['đéo','con cặc','giết', 'chết mẹ', 'mày', 'mẹ mày', 'cha mày', 'đồ chó', 'chó để', 'đụ má', 'đụ', 'vãi lồn', 'lồn', 'fuck','bitch','đĩ']
export const checkWord = (paragraph)=>{
    let result = false
   wordsBan.forEach(word => {
        if (paragraph.toLocaleLowerCase().indexOf(word) !== -1)    result = true     
   })
   return result
}
