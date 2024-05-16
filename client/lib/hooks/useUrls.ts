import axiosInstance from '@/services/api'
import { IUrls } from '@/types/urls.interface'
import { useQuery } from '@tanstack/react-query'

export const useUrls = () => {
  const { data, isLoading, refetch } = useQuery<Array<IUrls>>({
    queryKey: ['urls'],
    queryFn: async () => {
      const response = await axiosInstance.get('/shorturl')
      return response.data
    },
    staleTime: 6000,
  })

  return {
    data,
    isLoading,
    refetch,
  }
}
