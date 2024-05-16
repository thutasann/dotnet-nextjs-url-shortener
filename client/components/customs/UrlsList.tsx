import { useUrls } from '@/lib/hooks/useUrls'
import React from 'react'

function UrlsList() {
  const { data, isLoading, status } = useUrls()
  const host = 'localhost:5255'
  const protocol = typeof window !== 'undefined' && window.location.protocol

  return (
    <div className="mt-6 border-t border-slate-600 w-full pt-3">
      <h3 className="text-lg font-bold">URLs</h3>
      {status === 'error' && <p className="text-sm text-red-500">Something went wrong!</p>}
      {isLoading && status !== 'error' && <p className="text-sm animate-pulse">Loading...</p>}
      <ul>
        {data?.map((item) => {
          const url = `${protocol}//${host}/${item.shortUrl}`
          return (
            <li key={item.id} className="list-disc ml-4">
              <a className="text-blue-500 my-2 hover:text-blue-300 text-md  underline" href={url} target="_blank">
                {url}
              </a>
            </li>
          )
        })}
      </ul>
    </div>
  )
}

export default UrlsList
