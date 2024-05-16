import React, { useState } from 'react'
import { Input } from '../ui/input'
import { Button } from '../ui/button'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import axiosInstance from '@/services/api'
import { useToast } from '../ui/use-toast'

function UrlShortForm() {
  const [url, setUrl] = useState('')
  const [isValidUrl, setIsValidUrl] = useState(true)
  const { toast } = useToast()
  const queryClient = useQueryClient()

  const handleChange = (event: any) => {
    const { value } = event.target
    setUrl(value)
    setIsValidUrl(/^https?:\/\/\S+$/.test(value))
  }

  const { mutate } = useMutation({
    mutationFn: (payload: { url: string }) => {
      return axiosInstance.post(`/shorturl`, payload)
    },
    onSuccess: () => {
      toast({
        title: 'Success',
        description: 'URL is shortened..',
      })
      queryClient.invalidateQueries({ queryKey: ['urls'] })
    },
    onError: () => {
      toast({
        title: 'Something went wrong',
      })
    },
  })

  return (
    <section className="flex items-start gap-2 w-full">
      <div className="flex flex-col gap-2 w-full">
        <Input value={url} onChange={handleChange} placeholder="Enter Full Url" />
        {!isValidUrl && url && <div className="text-red-600 text-sm font-bold">Invalid URL</div>}
        {isValidUrl && url && <div className="text-green-400 text-sm font-bold">URL is valid!</div>}
      </div>
      <Button onClick={() => mutate({ url })} disabled={!isValidUrl} variant="secondary">
        Submit
      </Button>
    </section>
  )
}

export default UrlShortForm
